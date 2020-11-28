using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using CreateStair;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace CreateStairGH
{
    public class CreateStairGHComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public CreateStairGHComponent()
          : base("CreateStair", "CreateStair",
              "Description",
              "AECHandsOn", "GHTDD")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Height", "H", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Length", "L", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("TotalAngle", "A", "", GH_ParamAccess.item);
            pManager.AddIntegerParameter("NumberOfStairs", "NS", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("CenterLine", "C", "", GH_ParamAccess.item);
            pManager.AddLineParameter("StairLine", "S", "", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double h = 0.0;
            double l = 0.0;
            double tangle = 0.0;
            int numStair = 10;
            DA.GetData(0, ref h);
            DA.GetData(1, ref l);
            DA.GetData(2, ref tangle);
            DA.GetData(3, ref numStair);

            var result = StairCreator.CreateStair(h, l, tangle, numStair);

            DA.SetData(0, result.Item1);
            DA.SetDataList(1, new List<Line>(result.Item2));
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a635235d-d2d2-4637-b8ea-6665a0318a17"); }
        }
    }
}
