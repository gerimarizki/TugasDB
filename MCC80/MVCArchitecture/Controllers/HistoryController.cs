using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Controllers
{
    public class HistoryController
    {
        private History _historyModel;
        private VHistory _historyView;

        public HistoryController(History historyModel, VHistory historyView)
        {
            _historyModel = historyModel;
            _historyView = historyView;
        }

        public void GetAll()
        {
            var historyResult = _historyModel.GetAll();
            if (historyResult.Count() is 0)
            {
                _historyView.DataEmpty();
            }
            else
            {
                _historyView.GetAll(historyResult);
            }
        }

        public void Insert()
        {
            var history = _historyView.InsertMenu();

            var result = _historyModel.Insert(history);
            switch (result)
            {
                case -1:
                    _historyView.DataEmpty();
                    break;
                case 0:
                    _historyView.Fail();
                    break;
                default:
                    _historyView.Success();
                    break;
            }
        }

        public void Update()
        {
            var history = _historyView.UpdateMenu();
            var result = _historyModel.Update(history);

            switch (result)
            {
                case -1:
                    _historyView.DataEmpty();
                    break;
                case 0:
                    _historyView.Fail();
                    break;
                default:
                    _historyView.Success();
                    break;
            }
        }

        public void Delete()
        {
            var region = _historyView.DeleteMenu();
            var result = _historyModel.Delete(region);

            switch (result)
            {
                case -1:
                    _historyView.DataEmpty();
                    break;
                case 0:
                    _historyView.Fail();
                    break;
                default:
                    _historyView.Success();
                    break;
            }

        }

        public void SearchById()
        {
            DateTime id = _historyView.GetHistoryId();
            var result = _historyModel.GetById(id);
            if (result == null)
            {
                _historyView.DataEmpty();
            }
            else
            {
                _historyView.GetById(result);
            }
        }
    }
}
