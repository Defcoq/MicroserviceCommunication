using BookShop.Domain.Events;
using BookShop.Domain.Repositories;
using BookShop.Services.Configurations;
using BookShop.Services.Contracts;
using BookShop.Services.Mappers;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookMapper _bookMapper;
        private readonly ConnectionFactory _eventBusConnectionFactory;
        private readonly ILogger<BookService> _logger;
        private readonly EventBusSettings _settings;
        private readonly IEventBus _eventBus;

        public BookService(IBookRepository bookRepository, IBookMapper bookMapper, ConnectionFactory eventBusConnectionFactory,
ILogger<BookService> logger, EventBusSettings settings, IEventBus eventBus)
        {
            _bookRepository = bookRepository;
            _bookMapper = bookMapper;
            _eventBusConnectionFactory = eventBusConnectionFactory;
            _logger = logger;
            _settings = settings;
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }
        public async Task<BookResponse> AddBookAsync(AddBookRequest request)
        {
            var item = _bookMapper.Map(request);
            var result = _bookRepository.Add(item);
            await _bookRepository.UnitOfWork.SaveChangesAsync();
            return _bookMapper.Map(result);
        }

        public async Task<BookResponse> DeleteBookAsync(DeleteBookRequest request)
        {

            if (request?.Id == null) throw new ArgumentNullException();

            var result = await _bookRepository.GetAsync(request.Id);
            result.IsInactive = true;

            _bookRepository.Update(result);
            await _bookRepository.UnitOfWork.SaveChangesAsync();

            _eventBus.Publish(new BookDeleteIntegrationEvent(request.Id.ToString())); 
            SendDeleteMessage(new ItemSoldOutEvent { Id = request.Id.ToString() });
            return _bookMapper.Map(result);
        }

        public async Task<BookResponse> EditBookAsync(EditBookRequest request)
        {
            var existingRecord = await _bookRepository.GetAsync(request.Id);
            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.Id} is not present");
            }
            var entity = _bookMapper.Map(request);
            var result = _bookRepository.Update(entity);
            await _bookRepository.UnitOfWork.SaveChangesAsync();
            return _bookMapper.Map(result);
        }

        public async Task<BookResponse> GetBookAsync(GetBookRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var entity = await _bookRepository.GetAsync(request.Id);
            return _bookMapper.Map(entity);
        }

        public async Task<IEnumerable<BookResponse>> GetBooksAsync()
        {
            var result = await _bookRepository.GetAsync();
            return result.Select(x => _bookMapper.Map(x));
        }

        private void SendDeleteMessage(ItemSoldOutEvent message)
        {
            try
            {
                var connection = _eventBusConnectionFactory.CreateConnection();

                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: _settings.EventQueue, true, false);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: _settings.EventQueue, body: body);
                channel.WaitForConfirmsOrDie();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Unable to initialize the event bus: {message}", e.Message);
            }
        }
    }
}
