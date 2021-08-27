<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128657692/12.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4420)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/Q453145/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/Q453145/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/Q453145/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/Q453145/MainWindow.xaml.vb))
<!-- default file list end -->
# Drag & Drop - Copying rows instead of moving them when the CTRL key is pressed


<p>This example demonstrates how to allow a user to copy dragged rows on the modifier key press. A custom attached property is used to determine whether or not the user wants to copy rows. You can adjust this property in the PreviewKeyDown and PreviewKeyUp event handlers. Then, use it in the DragElementTemplate to modify the drag element appearance accordingly. At last, replace data objects within the DraggedRows collection in the Drop event handler with newly created items.</p>

<br/>


