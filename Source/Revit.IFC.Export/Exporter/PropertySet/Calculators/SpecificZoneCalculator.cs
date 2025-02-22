﻿//
// BIM IFC library: this library works with Autodesk(R) Revit(R) to export IFC files containing model geometry.
// Copyright (C) 2012  Autodesk, Inc.
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB.IFC;
using Autodesk.Revit.DB;
using Revit.IFC.Export.Utility;

namespace Revit.IFC.Export.Exporter.PropertySet.Calculators
{
   class SpecificZoneCalculator : PropertyCalculator
   {
      /// <summary>
      /// A string list to keep the calculated value.
      /// </summary>
      private List<string> m_SpecificZones = new List<string>();

      /// <summary>
      /// A static instance of this class.
      /// </summary>
      static SpecificZoneCalculator s_Instance = new SpecificZoneCalculator();

      /// <summary>
      /// The SpecificZoneCalculator instance.
      /// </summary>
      public static SpecificZoneCalculator Instance
      {
         get { return s_Instance; }
      }

      /// <summary>
      /// Calculates specific zones for a space.
      /// </summary>
      /// <param name="exporterIFC">The ExporterIFC object.</param>
      /// <param name="extrusionCreationData">The IFCExtrusionCreationData.</param>
      /// <param name="element">The element to calculate the value.</param>
      /// <param name="elementType">The element type.</param>
      /// <returns>True if the operation succeed, false otherwise.</returns>
      public override bool Calculate(ExporterIFC exporterIFC, IFCExtrusionCreationData extrusionCreationData, Element element, ElementType elementType, EntryMap entryMap)
      {
         string basePropSpecZoneString = "Project Specific Zone";
         int val = 0;
         while (++val < 1000)   // prevent infinite loop.
         {
            string propSpecZoneString;
            if (val == 1)
               propSpecZoneString = basePropSpecZoneString;
            else
               propSpecZoneString = basePropSpecZoneString + " " + val;

            string value;
            if ((ParameterUtil.GetStringValueFromElementOrSymbol(element, propSpecZoneString, out value) == null)
                || string.IsNullOrEmpty(value))
               break;

            m_SpecificZones.Add(value);
         }

         return m_SpecificZones.Count > 0;
      }

      public override bool CalculatesMutipleValues
      {
         get { return true; }
      }

      /// <summary>
      /// Gets the calculated string values.
      /// </summary>
      /// <returns>
      /// The string values.
      /// </returns>
      public override IList<string> GetStringValues()
      {
         return m_SpecificZones;
      }
   }
}