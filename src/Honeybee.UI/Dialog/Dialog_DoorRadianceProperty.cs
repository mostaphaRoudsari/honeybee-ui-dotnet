﻿using Eto.Drawing;
using Eto.Forms;
using System.Linq;
using HB = HoneybeeSchema;
using System;
using EnergyLibrary = HoneybeeSchema.Helper.EnergyLibrary;
using HoneybeeSchema;

namespace Honeybee.UI
{
    public class Dialog_DoorRadianceProperty : Dialog<HB.DoorRadiancePropertiesAbridged>
    {
     
        public Dialog_DoorRadianceProperty(HB.DoorRadiancePropertiesAbridged doorRadianceProperties, bool updateChangesOnly = false)
        {
            try
            {
                var prop = doorRadianceProperties ?? new HB.DoorRadiancePropertiesAbridged();

                if (updateChangesOnly)
                    prop = new HB.DoorRadiancePropertiesAbridged("No Changes");


                Padding = new Padding(15);
                Title = "Door Radiance Properties - Honeybee";
                WindowStyle = WindowStyle.Default;
                Width = 450;
                this.Icon = DialogHelper.HoneybeeIcon;

                //Get Modifier
                var mSets = EnergyLibrary.InModelRadianceProperties.Modifiers
                    .OfType<HB.IDdRadianceBaseModel>()
                    .ToList();

                if (updateChangesOnly)
                    mSets.Insert(0, new HB.Plastic("No Changes"));

                var modifierDP = DialogHelper.MakeDropDown(prop.Modifier, (v) => prop.Modifier = v?.Identifier,
                    mSets, "Default Modifier");

                var modifierBlkDP = DialogHelper.MakeDropDown(prop.ModifierBlk, (v) => prop.ModifierBlk = v?.Identifier,
                    mSets, "Default Modifier");

                DefaultButton = new Button { Text = "OK" };
                DefaultButton.Click += (sender, e) => 
                {
                    Close(prop); 
                };

                AbortButton = new Button { Text = "Cancel" };
                AbortButton.Click += (sender, e) => Close();

           
                var layout = new DynamicLayout();
                //layout.DefaultPadding = new Padding(10);
                layout.DefaultSpacing = new Size(5, 5);

                layout.AddRow("Door Modifier:");
                layout.AddRow(modifierDP);
                layout.AddRow("Door Modifier Blk:");
                layout.AddRow(modifierBlkDP);
                layout.AddSeparateRow(null, this.DefaultButton, this.AbortButton, null);
                layout.AddRow(null);
                //Create layout
                Content = layout;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Failed to open DoorRadianceProperty dialog:\n{e.Message}");
            }
            
            
        }
    
    
    }
}
