using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Controllers
{
    public class LocationController
    {
        private Location _locationModel;
        private VLocation _locationView;

        public LocationController(Location locationModel, VLocation locationView)
        {
            _locationModel = locationModel;
            _locationView = locationView;
        }

        public void GetAll()
        {
            var locationResult = _locationModel.GetAll();
            if (locationResult.Count() is 0)
            {
                _locationView.DataEmpty();
            }
            else
            {
                _locationView.GetAll(locationResult);
            }
        }

        public void Insert()
        {
            var location = _locationView.InsertMenu();

            var result = _locationModel.Insert(location);
            switch (result)
            {
                case -1:
                    _locationView.DataEmpty();
                    break;
                case 0:
                    _locationView.Fail();
                    break;
                default:
                    _locationView.Success();
                    break;
            }
        }

        public void Update()
        {
            var location = _locationView.UpdateMenu();
            var result = _locationModel.Update(location);

            switch (result)
            {
                case -1:
                    _locationView.DataEmpty();
                    break;
                case 0:
                    _locationView.Fail();
                    break;
                default:
                    _locationView.Success();
                    break;
            }
        }

        public void Delete()
        {
            var location = _locationView.DeleteMenu();
            var result = _locationModel.Delete(location);

            switch (result)
            {
                case -1:
                    _locationView.DataEmpty();
                    break;
                case 0:
                    _locationView.Fail();
                    break;
                default:
                    _locationView.Success();
                    break;
            }

        }

        public void SearchById()
        {
            int id = _locationView.GetLocationId();
            var result = _locationModel.GetById(id);
            if (result == null)
            {
                _locationView.DataEmpty();
            }
            else
            {
                _locationView.GetById(result);
            }
        }
    }
}
