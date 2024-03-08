using Azure;
using BindingData_SQL_EF.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BindingData_SQL_EF.Shared.Services
{
	public class BookServices
	{
		private readonly HttpClient _httpClient;

		public BookServices(HttpClient httpClient)
		{
			_httpClient = httpClient;

		}

		public async Task<List<Book>> GetBooks()
		{
			var result = await _httpClient.GetFromJsonAsync<List<Book>>("api/Book");
			
			return result ?? new List<Book>();
		}


		public async Task<bool> InsertBook(Book value)
		{
			var response = await _httpClient.PostAsJsonAsync<Book>($"api/Book", value);
			if (response.IsSuccessStatusCode)
				return true;
			else
				return false;
		}
		public async Task<bool> RemoveBook(long bookId)
		{
			HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Book/{bookId}");
			if (response.IsSuccessStatusCode)
				return true;
			else
				return false;
		}

		public async Task<bool> UpdateBook(long bookId, Book updatedBook)
		{
			HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Book/{bookId}", updatedBook);
			if (response.IsSuccessStatusCode)
				return true;
			else
				return false;

		}
	}
}
