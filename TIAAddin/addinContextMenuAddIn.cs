using System.IO;
using System.Windows.Forms;
using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;

namespace TIAAddin
{
    public class addinContextMenuAddIn : ContextMenuAddIn
    {
        /// <summary>
        /// The instance of TIA Portal in which the Add-In works.
        /// <para>Enables Add-In to interact with TIA Portal.</para>
        /// </summary>
        readonly TiaPortal m_TiaPortal;

        /// <summary>
        /// The display name of the Add-In.
        /// TODO: Enter your display name here
        /// </summary>
        private const string s_DisplayNameOfAddIn = "addinContextMenuAddin";

        /// <summary>
        /// The constructor of the addinContextMenuAddIn.
        /// Creates an instance of the class addinContextMenuAddIn.
        /// <para>- Called from addinAddInProvider when the first right-click is performed in TIA Portal.</para>
        /// <para>- The base class' constructor of ContextMenuAddIn will also be executed.</para>
        /// </summary>
        /// <param name="tiaPortal">
        /// Represents the instance of TIA Portal in which the Add-In will work.
        /// </param>
        public addinContextMenuAddIn(TiaPortal tiaPortal) : base(s_DisplayNameOfAddIn)
        {
            m_TiaPortal = tiaPortal;
        }

        /// <summary>
        /// The method is provided to create a submenu of the Add-In's context menu item.
        /// Called when a mouse-over is performed on the Add-In's context menu item.
        /// </summary>
        /// <param name="addInRootSubmenu">
        /// Submenu of the Add-In's context menu item.
        /// </param>
        /// <example>
        /// ActionItems can be created with or without a checkbox or a radiobutton.
        /// In this example, only simple ActionItems will be created, which will start the Add-In program code.
        /// </example>
        protected override void BuildContextMenuItems(ContextMenuAddInRoot addInRootSubmenu)
        {
            /* Method addInRootSubmenu.Items.AddActionItem
            *  will create a new context menu item with specified text
            *  - its 1st input parameter is the label text of the context menu item;
            *  - its 2nd input parameter is the delegate, which will be executed when the context menu item is clicked;
            *  - its 3rd input parameter is the delegate, which will be executed when the mouse is over the context menu item;
            *  - its generic type parameter (inside the  "<>"-brackets) is the type of AddActionItem,
            *    e.g. AddActionItem<DeviceItem> will create a context menu item that will be displayed on a right-click on a DeviceItem,
            *    whereas AddActionItem<Project> will create a context menu item that will be displayed on a right-click on the project name.
            */
            // TODO: Change the code here
            // Example: 
            addInRootSubmenu.Items.AddActionItem<IEngineeringObject>("Action 1", OnDoSomething1, OnCanSomething1);
            addInRootSubmenu.Items.AddActionItem<IEngineeringObject>("Action 2", OnDoSomething2, OnCanSomething2);
        }
        internal static class Utility
        {
            internal static Form GetForegroundWindow()
            {
                // Workaround for Add-In windows to be shown in foreground of TIA Portal
                Form form = new Form { Opacity = 0, ShowIcon = false };
                form.Show();
                form.TopMost = true;
                form.Activate();
                form.TopMost = false;
                return form;
            }
        }

        /// <summary>
        /// The method contains the program code of the TIA Add-In.
        /// Called when the context menu item 'Action 1' (added in the body of the method BuildContextMenuItems) is chosen.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be IEngineeringObject)
        /// </param>
        private void OnDoSomething1(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            // TODO: Change the code here
            // Program of AddIn
            using (Form owner = Utility.GetForegroundWindow())
            {
                OpenFileDialog();
                MessageBox.Show("Hello from addinContextMenuAddin", "Action 1");
            }
        }
        public static string OpenFileDialog(string filter = "All files (*.*)|*.*", string initialDirectory = "")
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = filter;
                openFileDialog.FilterIndex = 1;

                if (!string.IsNullOrEmpty(initialDirectory) && Directory.Exists(initialDirectory))
                {
                    openFileDialog.InitialDirectory = initialDirectory;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }

                return null;
            }
        }
        /// <summary>
        /// Called when mouse is over the context menu item 'Action 1'.
        /// The returned value will be used to enable or disable it.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be IEngineeringObject)
        /// </param>
        private MenuStatus OnCanSomething1(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            // TODO: Change the code here
            // MenuStatus
            //  Enabled  = Visible
            //  Disabled = Visible but not executable
            //  Hidden   = Item will not be shown
            return MenuStatus.Enabled;
        }

        /// <summary>
        /// The method contains the program code of the Add-In.
        /// Called when the context menu item 'Action 2' (added in the body of the method BuildContextMenuItems) is chosen.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be IEngineeringObject)
        /// </param>
        private void OnDoSomething2(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            // TODO: Change the code here
            // Program of AddIn
            MessageBox.Show("Hello from addinContextMenuAddin", "Action 2");
        }

        /// <summary>
        /// Called when mouse is over the context menu item 'Action 2'.
        /// The returned value will be used to enable or disable it.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be IEngineeringObject)
        /// </param>
        private MenuStatus OnCanSomething2(MenuSelectionProvider<IEngineeringObject> menuSelectionProvider)
        {
            // TODO: Change the code here
            // MenuStatus
            //  Enabled  = Visible
            //  Disabled = Visible but not executable
            //  Hidden   = Item will not be shown
            return MenuStatus.Enabled;
        }
    }
}
