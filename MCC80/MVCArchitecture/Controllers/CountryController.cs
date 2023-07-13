using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Controllers
{
    public class CountryController
    {
        private Country _countryModel;
        private VCountry _countryView;

        public CountryController(Country countryModel, VCountry countryView)
        {
            _countryModel = countryModel;
            _countryView = countryView;
        }

        public void GetAll()
        {
            var regionResult = _countryModel.GetAll();
            if (regionResult.Count() is 0)
            {
                _countryView.DataEmpty();
            }
            else
            {
                _countryView.GetAll(regionResult);
            }
        }

        public void Insert()
        {
            var region = _countryView.InsertMenu();

            var result = _countryModel.Insert(region);
            switch (result)
            {
                case -1:
                    _countryView.DataEmpty();
                    break;
                case 0:
                    _countryView.Fail();
                    break;
                default:
                    _countryView.Success();
                    break;
            }
        }

        public void Update()
        {
            var region = _countryView.UpdateMenu();
            var result = _countryModel.Update(region);

            switch (result)
            {
                case -1:
                    _countryView.DataEmpty();
                    break;
                case 0:
                    _countryView.Fail();
                    break;
                default:
                    _countryView.Success();
                    break;
            }
        }

        public void Delete()
        {
            var region = _countryView.DeleteMenu();
            var result = _countryModel.Delete(region);

            switch (result)
            {
                case -1:
                    _countryView.DataEmpty();
                    break;
                case 0:
                    _countryView.Fail();
                    break;
                default:
                    _countryView.Success();
                    break;
            }

        }
        public void SearchById()
        {
            string id = _countryView.GetCountryId();
            var result = _countryModel.GetById(id);
            if (result == null)
            {
                _countryView.DataEmpty();
            }
            else
            {
                _countryView.GetById(result);
            }
        }

    }
}