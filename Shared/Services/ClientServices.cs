using BindingData_SQL_EF.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BindingData_SQL_EF.Shared.Services
{
	public class ClientServices
	{
		private readonly HttpClient _httpClient;

		public ClientServices(HttpClient httpClient)
		{
			_httpClient = httpClient;

		}

		public async Task<List<Book>> GetBooks()
		{
			var result = await _httpClient.GetFromJsonAsync<List<Book>>("api/Book");

			return result;
		}


		public async Task<Book> InsertBook(Book value)
		{
			await _httpClient.PostAsJsonAsync<Book>($"api/Book/", value);

			return value;
		}
		public async Task<bool> RemoveBook(long bookId)
		{
			HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Book/{bookId}");

			return true;
		}

		public async Task<Book> UpdateBook(long bookId, Book updatedBook)
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Book/{bookId}", updatedBook);

			return updatedBook;

		}
	}
}
