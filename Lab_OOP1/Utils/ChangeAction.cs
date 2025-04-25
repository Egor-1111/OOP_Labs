using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP1
{
    public class ChangeAction
    {
        private  List<List<IFigure>> StorageAdd= new List<List<IFigure>>();
        private List<List<IFigure>> StorageDelete= new List<List<IFigure>>();
        private WorkWithFile w = new WorkWithFile();
        public bool AddToStorageDelete(List<IFigure>image)
        {
            if(StorageDelete.Count<7)
            {
                StorageDelete.Add(image);
            }
            else
            {
                StorageDelete.RemoveAt(0);
                StorageDelete.Add(image);
            }
            StorageAdd.Clear();
            return true;
        }

        public List<IFigure> Accept()
        {
            
            if (StorageAdd.Count > 0)
            {
                w.WriteInFile(StorageAdd[StorageAdd.Count - 1], "data1.json");
               
                StorageDelete.Add(w.ReadFromFile("data1.json"));
                List<IFigure> figures = StorageAdd[StorageAdd.Count - 1];
                StorageAdd.RemoveAt(StorageAdd.Count - 1);
                return figures;
            }
            else return null;
        }
        public List<IFigure> Decline()
        {
            if (StorageDelete.Count > 1)
            {
                w.WriteInFile(StorageDelete[StorageDelete.Count - 1], "data1.json");

                StorageAdd.Add(w.ReadFromFile("data1.json"));
                StorageDelete.RemoveAt(StorageDelete.Count - 1);
                List<IFigure> figures = StorageDelete[StorageDelete.Count - 1];

                return figures;
            }
            
            else return null;
        }
    }
}
