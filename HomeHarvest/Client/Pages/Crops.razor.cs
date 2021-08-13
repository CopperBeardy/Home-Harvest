using DevExpress.Blazor;
using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.Pages
{
	public partial class Crops
    {
        IEnumerable<CropDto> CropItems { get; set; }

        DxDataGrid<CropDto> grid { get; set; }

        CropDto SelectedRow { get; set; }

        bool Enabled = true;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
             SetSelection();
        }

        async Task OnEditClick()
        {
            await grid.StartRowEdit(SelectedRow);
        }

        async Task OnDeleteClick()
        {
            await CropRepository.Delete(SelectedRow.Id);
            await LoadData();
            if (CropItems.Count() == 0)
                ChangeToolbarEnabled(false);
            else
                SetSelection();
            StateHasChanged();
        }

        async Task OnRowRemovingAsync(CropDto dataItem)
        {
            await CropRepository.Delete(dataItem.Id);
            await LoadData();
            StateHasChanged();
        }

        async Task OnRowUpdatingAsync(CropDto dataItem, IDictionary<string, object> newValues)
        {
            dataItem.Location = (string)newValues[nameof(CropDto.Location)];
            await CropRepository.Update(dataItem.Id, dataItem);
            await LoadData();
            StateHasChanged();
        }

        void SetSelection()
        {
            SelectedRow = CropItems.FirstOrDefault();
        }

        void ChangeToolbarEnabled(bool enabled)
        {
            Enabled = enabled;
            StateHasChanged();
        }

        public async Task LoadData()
        {
            CropItems = await CropRepository.GetAll();
        }
    }
}