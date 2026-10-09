using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfApp2
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _service = new();
        private readonly ObservableCollection<ProductViewModel> _allProducts;

        public MainViewModel()
        {
            _allProducts = new ObservableCollection<ProductViewModel>(_service.LoadProducts());

            FilteredProducts = new ObservableCollection<ProductViewModel>(_allProducts);

            CurrentUserFullName = "Никифорова Весения Николаевна";
        }

        public string CurrentUserFullName { get; set; }

        private ObservableCollection<ProductViewModel> _filteredProducts;
        public ObservableCollection<ProductViewModel> FilteredProducts
        {
            get => _filteredProducts;
            set { _filteredProducts = value; OnPropertyChanged(); }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private void ApplyFilter()
        {
            var query = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var s = SearchText.Trim().ToLower();

                query = query.Where(p =>
                    (p.Article ?? "").ToLower().Contains(s) ||
                    (p.CategoryName ?? "").ToLower().Contains(s) ||
                    (p.Description ?? "").ToLower().Contains(s) ||
                    (p.ManufacturerName ?? "").ToLower().Contains(s) ||
                    (p.SupplierName ?? "").ToLower().Contains(s));
            }

            FilteredProducts = new ObservableCollection<ProductViewModel>(query);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}