using BindingData_SQL_EF.Shared.Models;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.Runtime;
using System.Text.Json;

namespace BindingData_SQL_EF.Client.Pages.Binding
{
	public partial class ListBooks
	{
		#region Variables

		Book Book_Model = new();
		List<Book>? LibraryBooks { get; set; }
		int Id { set; get; }
		string message = "";
		BindingData_SQL_EF.Shared.Models.Book order = new();
		IEnumerable<Book>? books;

		RadzenDataGrid<Book> grid;
		DataGridSettings _settings;

        IEnumerable<int> pageSizeOptions = new int[] { 4, 6, 8 };
		bool isLoading = false;
		int count;
		bool EstaCargando = false;
		int pageSize = 6;

		#endregion

		#region Methodos principal
		protected override async Task OnInitializedAsync()
		{
			await CargarDatos();
		}

		private async Task CargarDatos()
		{
			EstaCargando = true;
			await Task.Yield();

			LibraryBooks = await Booklibrary.GetBooks();

			if (LibraryBooks == null)
			{
				_ = DialogService.Alert("Error");
				return;
			}

			books = LibraryBooks;

			await Task.Delay(100);
			EstaCargando = false;

		}

		async Task EliminarBook(Book book_deleted)
		{
			var confirmResult = await DialogService.Confirm("Quieres eliminar?", "Elimenar datos?");

			if (!confirmResult.Value)
			{
				return;
			}
			var result = await Booklibrary.RemoveBook(book_deleted.Id);

			StateHasChanged();

			if (!result)
			{
				await DialogService.Alert("¡Error al eliminar datos!");
				return;
			}
			await CargarDatos();
			await grid.Reload();
			StateHasChanged();
		}



		#endregion
		#region DataGrud 

		public DataGridSettings Settings
		{
			get
			{
				return _settings;
			}
			set
			{
				if (_settings != value)
				{
					_settings = value;
					InvokeAsync(SaveStateAsync);
				}
			}
		}

		private async Task LoadStateAsync()
		{
			var result = await JSRuntime.InvokeAsync<string>("window.localStorage.getItem", "SettingsLoadData_ordre");
			if (!string.IsNullOrEmpty(result) && result != "null")
			{
				_settings = JsonSerializer.Deserialize<DataGridSettings>(result);
				if (_settings.PageSize.HasValue)
				{
					pageSize = _settings.PageSize.Value;
					await Task.Yield();
				}
			}
		}

		private async Task SaveStateAsync()
		{
			await JSRuntime.InvokeVoidAsync("window.localStorage.setItem", "SettingsLoadData_ordre", JsonSerializer.Serialize<DataGridSettings>(Settings));
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await LoadStateAsync();
			}
		}

		void LoadSettings(DataGridLoadSettingsEventArgs args)
		{
			if (Settings != null)
			{
				args.Settings = Settings;
			}
		}

		void OnFilter(DataGridColumnFilterEventArgs<Book> args)
		{
			_ = args.FilterValue;
			//......
		}

		#endregion
	}
}
