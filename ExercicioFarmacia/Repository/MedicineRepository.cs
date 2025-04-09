using FarmaciaExercicio.Entities;

namespace FarmaciaExercicio.Repository
{
    public class MedicineRepository
    {
        // tem que lembrar de baixar os pacotes nuget do jason

        List<Medicine> _medicines = [];
        // criando uma NOVA lista de < Medicine >, criando uma variavel temporária _medicines
        private readonly string _dataBasePath;
        // string com o caminho do banco. Nao entendi porque dessa forma, perguntar pro profe.
        public MedicineRepository(string dataBasePath)
        {
            _dataBasePath = dataBasePath;
            LoadList();
        }

        private void LoadList()
        {
            string data = File.ReadAllText(_dataBasePath);

            _medicines = JsonConvert.DeserializeObject<List<Medicine>>(data) ?? [];
        }

        private void SaveMed()
        {
            string data = JsonConvert.SerializeObject(_medicines);

            File.WriteAllText(_dataBasePath, data);
        }

        public void AddMed(Medicine medicine)
        {
            _medicines.Add(medicine);
            SaveMed();
        }

        public void RemoveMed(Medicine medicine)
        {
            _medicines.Remove(medicine);
            SaveMed();
        }

        public Medicine SearchByName(string name)
        {
            return _medicines
                  .FirstOrDefault(medicine => medicine.Name == name,
                  null);
        }

        public Medicine SearchByID(string productID)
        {
            return _medicines
                  .FirstOrDefault(medicine => medicine.ProductID == productID,
                  null);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            Medicine givenMedicine = SearchByID(medicine.ProductID);
            if (givenMedicine is not null)
            {
                givenMedicine.Name = medicine.Name;
                givenMedicine.Laboratory = medicine.Laboratory;
                givenMedicine.ProductID = medicine.ProductID;
                givenMedicine.Category = medicine.Category;
                givenMedicine.Weight = medicine.Weight;
                givenMedicine.Price = medicine.Price;
                givenMedicine.Expiration = medicine.Expiration;

                SaveMed();
            }
        }

        public List<Medicine> FullSearch()
        {
            return _medicines;
        }
    }
}
