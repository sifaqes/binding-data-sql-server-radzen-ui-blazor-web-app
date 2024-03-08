using BindingData_SQL_EF.Shared.Models;
using BindingData_SQL_EF.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using static System.Net.WebRequestMethods;

namespace BindingData_SQL_EF.Client.Pages.Binding
{
	public partial class EditBooks
	{
		[Parameter]
		public int IdBook { get; set; }

		#region Variables
		Book book_model = new();
		IEnumerable<Book>? Book_data;
		RadzenDropDownDataGrid<Book> grid;
		private bool estaGuardado, EstaCargando, btnBorrar = false;
		bool available {
			get => book_model.Available.HasValue;
			set => book_model.Available = value;
		}
		#endregion

		#region Methodos principal
		protected override async Task OnInitializedAsync()
		{

			EstaCargando = true;
			if (IdBook != 0)
			{  //upload
				btnBorrar = true;
				var result = (await BookServices.GetBooks()).First(order => order.Id == IdBook);
				book_model = result;
				await CargaRelacionado();
			}
			else
			{ //Nuevo
				
				await CargaRelacionado();
			}
			EstaCargando = false;
		}

		public async Task CargaRelacionado()
		{

			//var result_Product = await BookServices.GetBooks();
			//if (result_Product == null)
			//	return;
			//Book_data = result_Product;
		}

		public async Task Guardar(Book data)
		{
			if (string.IsNullOrEmpty(data.Name))
			{
				NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Summary = "Notificación", Detail = "Falta nombre de producto", Duration = 2000 });
				return;
			}

			estaGuardado = true;

			if (IdBook != 0)
			{
				// Editar 
				var result = await BookServices.UpdateBook(IdBook, data);

				if (result)
				{
					await DialogService.Alert("Datos registrados correctamente!");
				}
				else
				{
					await DialogService.Alert("Ha corredo un error!");
				}
			}

			else
			{
				// Nuevo
				var result = await BookServices.InsertBook(data);
				if (result)
				{
					await DialogService.Alert("Datos registrados correctamente!");
				}
				else
				{
					await DialogService.Alert("Ha corredo un error!");
				}
			}
			estaGuardado = false;
		}


		public async Task VaciarProducto(Book miempleado)
		{
			//Reset List
			book_model = new Book();
			NotificationService.Notify(new NotificationMessage
			{
				Severity = NotificationSeverity.Success,
				Summary = "Empty",
				Detail = "Info Detail",
				Duration = 1000
			});

		}
		protected async Task EliminarEmpleado(long id)
		{
			var confirmResult = await DialogService.Confirm("Quieres eliminar?", "Elimenar datos?");
			if (confirmResult.HasValue && confirmResult.Value)
			{
				var result = await BookServices.RemoveBook(id);
				if (result)
				{
					_ = await DialogService.Alert("Borrado exitosamente!");
					await JSRuntime.InvokeVoidAsync("history.back");
				}
			}
		}

		#endregion
	}
}
