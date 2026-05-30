using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace housing
{
    public class HousingLogic
    {
        public static List<Housing> FilerBySurname(List<Housing> sourceList, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return sourceList;
            }

            return sourceList.Where(h => h.surname.ToLower().Contains(searchText.ToLower())).ToList();
        }

        public static List<Housing> FilterByArea(List<Housing> sourceList, int minArea)
        {
            return sourceList.Where(h => h.area >=  minArea).ToList();
        }
    }
}
