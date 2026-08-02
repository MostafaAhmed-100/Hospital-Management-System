using AutoMapper;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.DTOs.PaymentDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PaymentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<PaymentResponseDto>>> GetAllPaymentsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Payments.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<PaymentResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<PaymentResponseDto>>
                {
                    Message = "Payments retrieved successfully.",
                    Data = new PagedResultDto<PaymentResponseDto>
                    {
                        Items = mappedItems,
                        TotalCount = totalCount,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all payments.");
                throw;
            }
        }

        public async Task<ApiResponseDto<PaymentResponseDto>> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _unitOfWork.Payments.GetByIdAsync(id);

                if (payment == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Payment {PaymentId}.", id);
                    throw new KeyNotFoundException("The payment record does not exist.");
                }

                return new ApiResponseDto<PaymentResponseDto>
                {
                    Message = "Payment retrieved successfully.",
                    Data = _mapper.Map<PaymentResponseDto>(payment)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Payment {PaymentId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<PaymentResponseDto>>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var payments = await _unitOfWork.Payments.GetPaymentsByDateRangeAsync(startDate, endDate);

                return new ApiResponseDto<IEnumerable<PaymentResponseDto>>
                {
                    Message = "Payments retrieved successfully for the specified date range.",
                    Data = _mapper.Map<IEnumerable<PaymentResponseDto>>(payments)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving payments between {StartDate} and {EndDate}.", startDate, endDate);
                throw;
            }
        }

        public async Task<ApiResponseDto<PaymentResponseDto>> CreatePaymentAsync(CreatePaymentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var invoice = await _unitOfWork.Invoices.GetByIdAsync(dto.InvoiceId);
                if (invoice == null) throw new KeyNotFoundException("The specified invoice does not exist.");

                var payment = _mapper.Map<Payment>(dto);
                payment.PaymentDate = DateTime.UtcNow;

                await _unitOfWork.Payments.AddAsync(payment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Payment {PaymentId}.", payment.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<PaymentResponseDto>
                {
                    Message = "Payment created successfully.",
                    Data = _mapper.Map<PaymentResponseDto>(payment)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new payment.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdatePaymentAsync(UpdatePaymentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var payment = await _unitOfWork.Payments.GetByIdAsync(dto.Id);
                if (payment == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Payment {PaymentId}.", dto.Id);
                    throw new KeyNotFoundException("The payment record does not exist.");
                }

                _mapper.Map(dto, payment);

                _unitOfWork.Payments.Update(payment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Payment {PaymentId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Payment updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Payment {PaymentId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeletePaymentAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var payment = await _unitOfWork.Payments.GetByIdAsync(id);

                if (payment == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Payment {PaymentId}.", id);
                    throw new KeyNotFoundException("The payment record does not exist.");
                }

                _unitOfWork.Payments.Delete(payment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Payment {PaymentId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Payment deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Payment {PaymentId}.", id);
                throw;
            }
        }
    }
}