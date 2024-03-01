using Core.Abstracts;
using Entity.DTOs.Orders;
using Entity.Entities;
using DataAccess.Abstracts;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Business.Validations.Orders;
using Core.CrossCuttingConcerns.Validation;
using Entity.DTOs.ProductTransactions;
using Core.Exceptions;
using Entity.ViewModels.Orders;
using Business.Concretes.Common;
using Core.Aspects.Autofac.Security;
using Core.CrossCuttingConcerns.Security;

namespace Core.Concretes;

[MustBeAuthorized]
public class OrderManager(IOrderRepository orderRepository,
                          OrderValidations orderValidations,
                          IProductTransactionService productTransactionService,
                          IOrderDetailService orderDetailService)
    : ManagerBase, IOrderService
{
    [LogAspect]
    [SecurityAspect]
    [TransactionScopeAspect]
    [ValidationAspect(typeof(OrderAddValidations))]
    public void Add(OrderAddDto orderAddDto)
    {
        var order = orderRepository.Add(new()
        {
            UserId = orderAddDto.UserId,
            CreatedDate = DateTime.UtcNow
        });

        // ProductTransaction listesinde varsa tekrar eden ProductId'leri birleştirerek productId bazında tek bir OrderDetail oluşmasını sağlamak için;
        var productTransactions = orderAddDto.ProductTransactions
                                             .GroupBy(pt => pt.ProductId)
                                             .Select(group =>
                                             {
                                                 return new ProductTransactionAddDto()
                                                 {
                                                     ProductId = group.Key,
                                                     Quantity = group.Sum(pt => pt.Quantity)
                                                 };
                                             }).ToList();

        productTransactions.ForEach(pt =>
        {
            pt.Quantity *= -1;
            pt.CreatedDate = DateTime.UtcNow;

            var addedProductTransaction = productTransactionService.Add(pt);

            orderDetailService.Add(new()
            {
                OrderId = order.Id,
                ProductTransactionId = addedProductTransaction.Id,
                Status = "Preparing"
            });
        });
    }

    [LogAspect]
    [SecurityAspect]
    [ValidationAspect(typeof(OrderAddValidations))]
    [TransactionScopeAspect]
    public async Task AddAsync(OrderAddDto orderAddDto)
    {
        var order = orderRepository.Add(new()
        {
            UserId = orderAddDto.UserId,
            CreatedDate = DateTime.UtcNow
        });

        // ProductTransaction listesinde varsa tekrar eden ProductId'leri birleştirerek productId bazında tek bir OrderDetail oluşmasını sağlamak için;
        var productTransactions = orderAddDto.ProductTransactions
                                             .GroupBy(pt => pt.ProductId)
                                             .Select(group =>
                                             {
                                                 return new ProductTransactionAddDto()
                                                 {
                                                     ProductId = group.Key,
                                                     Quantity = group.Sum(pt => pt.Quantity)
                                                 };
                                             });

        foreach (var pt in productTransactions)
        {
            pt.Quantity *= -1;
            pt.CreatedDate = DateTime.UtcNow;

            var addedProductTransaction = await productTransactionService.AddAsync(pt);

            await orderDetailService.AddAsync(new()
            {
                OrderId = order.Id,
                ProductTransactionId = addedProductTransaction.Id,
                Status = "Preparing"
            });
        }
    }

    [SecurityAspect]
    [ValidationAspect(typeof(OrderDeleteValidations))]
    public void DeleteById(Guid id)
    {
        orderRepository.Delete(ValidationReturn.Entity);
    }

    [SecurityAspect]
    [ValidationAspect(typeof(OrderDeleteValidations))]
    public async Task DeleteByIdAsync(Guid id)
    {
        await orderRepository.DeleteAsync(ValidationReturn.Entity);
    }

    [SecurityAspect]
    public IEnumerable<OrderListVm> GetAll()
    {
        return OrderListVm.GetModels(orderRepository.GetAll());
    }

    [SecurityAspect]
    public async Task<IEnumerable<OrderListVm>> GetAllAsync()
    {
        return OrderListVm.GetModels(await orderRepository.GetAllAsync());
    }

    [SecurityAspect]
    [ValidationAspect(typeof(OrderValidations))]
    public OrderVm GetById(Guid id)
    {
        return OrderVm.GetModel(ValidationReturn.Entity);
    }

    [SecurityAspect]
    [ValidationAspect(typeof(OrderValidations))]
    public async Task<OrderVm> GetByIdAsync(Guid id)
    {
        return OrderVm.GetModel(ValidationReturn.Entity);
    }

    [SecurityAspect]
    [TransactionScopeAspect]
    [ValidationAspect(typeof(OrderUpdateValidations))]
    public void Update(Guid id, OrderUpdateDto orderUpdateDto)
    {
        Order order = ValidationReturn.Entity;

        order.UserId = orderUpdateDto.UserId;
        orderRepository.Update(order);

        // ProductId bazında tekrarlanan verileri birleştirmek için;
        var productTransactions = orderUpdateDto.ProductTransactions.GroupBy(pt => pt.ProductId).Select(group =>
        {
            return new ProductTransactionUpdateDto()
            {
                ProductId = group.Key,
                Quantity = group.Sum(pt => pt.Quantity)
            };
        }).ToList();

        foreach (var od in order.OrderDetails)
        {
            // Sadece status'ü "Preparing" olan productTransaction'lar güncellenebilir diyelim;
            var productTransaction = productTransactions.Find(pt => pt.ProductId == od.ProductTransaction.ProductId && od.Status == "Preparing");

            if (productTransaction != null)
            {
                var beUpdatedPt = od.ProductTransaction;

                var stockBeforeOrderIsAdded = productTransactionService.GetStockByProductId(beUpdatedPt.ProductId) - beUpdatedPt.Quantity;

                beUpdatedPt.Quantity = -1 * productTransaction.Quantity;

                if (stockBeforeOrderIsAdded + beUpdatedPt.Quantity < 0)
                    throw new ValidationException($"There is not enough stock for {productTransaction.ProductId}");

                productTransactionService.Update(beUpdatedPt.Id, ProductTransactionUpdateDto.GetModel(beUpdatedPt));
            }
        }
    }

    [SecurityAspect]
    [TransactionScopeAspect]
    [ValidationAspect(typeof(OrderUpdateValidations))]
    public async Task UpdateAsync(Guid id, OrderUpdateDto orderUpdateDto)
    {
        Order order = ValidationReturn.Entity;

        order.UserId = orderUpdateDto.UserId;
        await orderRepository.UpdateAsync(order);

        // ProductId bazında tekrarlanan verileri birleştirmek için;
        var productTransactions = orderUpdateDto.ProductTransactions.GroupBy(pt => pt.ProductId).Select(group =>
        {
            return new ProductTransactionUpdateDto()
            {
                ProductId = group.Key,
                Quantity = group.Sum(pt => pt.Quantity)
            };
        }).ToList();

        foreach (var od in order.OrderDetails)
        {
            // Sadece status'ü "Preparing" olan productTransaction'lar güncellenebilir diyelim;
            var productTransaction = productTransactions.Find(pt => pt.ProductId == od.ProductTransaction.ProductId && od.Status == "Preparing");

            if (productTransaction != null)
            {
                var beUpdatedPt = od.ProductTransaction;

                var stockBeforeOrderIsAdded = (await productTransactionService.GetStockByProductIdAsync(beUpdatedPt.ProductId)) - beUpdatedPt.Quantity;

                beUpdatedPt.Quantity = -1 * productTransaction.Quantity;

                if (stockBeforeOrderIsAdded + beUpdatedPt.Quantity < 0)
                    throw new ValidationException($"There is not enough stock for {productTransaction.ProductId}");

                await productTransactionService.UpdateAsync(beUpdatedPt.Id, ProductTransactionUpdateDto.GetModel(beUpdatedPt));
            }
        }
    }
}