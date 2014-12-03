/*
' Copyright (c) 2014 Christoc.com Software Solutions
'  All rights reserved.
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
' and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT 
' SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN 
' ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
' OR OTHER DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Entities.Users;
using Christoc.Modules.HomeController.Components;
using DotNetNuke.Services.Exceptions;

namespace Christoc.Modules.HomeController
{
    /// -----------------------------------------------------------------------------
    /// <summary>   
    /// The Edit class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from HomeControllerModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : HomeControllerModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Implement your edit logic for your module
                if (!Page.IsPostBack)
                {
                    //check if we have an ID passed in via a querystring parameter, if so, load that Device to edit.
                    //DeviceId is defined in the DeviceModuleBase.cs file
                    if (DeviceId > 0)
                    {
                        var tc = new DeviceController();

                        var t = tc.GetDevice(DeviceId, ModuleId);
                        if (t != null)
                        {
                            txtName.Text = t.DeviceName;
                            txtDescription.Text = t.DeviceDescription;
                        }
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var t = new Device();
            var tc = new DeviceController();

            //TODO: populate device properties

            if (DeviceId > 0)
            {
                t = tc.GetDevice(DeviceId, ModuleId);
                t.DeviceName = txtName.Text.Trim();
                t.DeviceDescription = txtDescription.Text.Trim();
                t.LastModifiedByUserId = UserId;
                t.LastModifiedOnDate = DateTime.Now;
                
            }
            else
            {
                t = new Device()
                {
                    CreatedByUserId = UserId,
                    CreatedOnDate = DateTime.Now,
                    DeviceName = txtName.Text.Trim(),
                    DeviceDescription = txtDescription.Text.Trim(),

                };
            }

            t.LastModifiedOnDate = DateTime.Now;
            t.LastModifiedByUserId = UserId;
            t.ModuleId = ModuleId;

            if (t.Id > 0)
            {
                tc.UpdateDevice(t);
            }
            else
            {
                tc.CreateDevice(t);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }
    }
}