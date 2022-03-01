using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_3_2
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedRefList = uidoc.Selection.PickObjects(ObjectType.Element, "Выберите элемент по грани");
            var elementList = new List<Element>();
            string info = string.Empty;
            double xref = 0;
            foreach (var selectedElement in selectedRefList)
            {
                Element element = doc.GetElement(selectedElement);
                elementList.Add(element);
                if (element is Pipe)
                {
                    Parameter Vparameter = element.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                    if (Vparameter.StorageType == StorageType.Double)
                    {
                        double VValue = UnitUtils.ConvertFromInternalUnits(Vparameter.AsDouble(), UnitTypeId.Meters);
                        xref += VValue;


                    }


                }

            }
            info += $"{xref}";
            TaskDialog.Show("Length", info);

            return Result.Succeeded;
        }
    }
}
