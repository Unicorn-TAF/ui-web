using System;
using System.Collections.Generic;
using System.Linq;
using Unicorn.UI.Core.Controls;
using Unicorn.UI.Core.Controls.Interfaces.Typified;
using Unicorn.UI.Core.Driver;

namespace Unicorn.UI.Web.Controls.Typified
{
    /// <summary>
    /// Default implementation for data grid described by next structure:<br/>
    /// 
    /// <code>
    /// &lt;table&gt;
    ///   &lt;thead&gt; 
    ///     &lt;th/&gt;&lt;th/&gt;&lt;th/&gt;
    ///   &lt;/thead&gt;
    ///   &lt;tbody&gt;
    ///     &lt;tr&gt;
    ///       &lt;td/&gt;&lt;td/&gt;&lt;td/&gt;
    ///     &lt;/tr&gt;
    ///   &lt;/tbody&gt;
    /// &lt;/table&gt;
    /// </code>
    /// Has definitions of of basic methods and properties.
    /// </summary>
    public class DataGrid : WebControl, IDataGrid
    {
        private const string ColumnCss = "thead th";
        private const string RowCss = "tbody tr";
        private const string ColumnXpathTemplate = ".//thead//th[normalize-space(.) = '{0}']";

        /// <summary>
        /// Immediately gets count of rows in table.
        /// </summary>
        public int RowsCount => HasRows ? Rows.Count : 0;

        private IList<DataGridRow> Rows => FindList<DataGridRow>(ByLocator.Css(RowCss));

        private bool HasRows => TryGetChild(ByLocator.Css(RowCss));

        /// <summary>
        /// Gets table cell located at specified row and column (zero based).
        /// </summary>
        /// <param name="rowIndex">row index (starts from 0)</param>
        /// <param name="columnIndex">column index (starts from 0)</param>
        /// <returns><see cref="DataGridCell"/> object as <see cref="IDataGridCell"/></returns>
        /// <exception cref="ControlNotFoundException">if cell was not found</exception>
        public IDataGridCell GetCell(int rowIndex, int columnIndex) =>
            GetRow(rowIndex).GetCell(columnIndex);

        /// <summary>
        /// Searches for row having specified value at specified column 
        /// and from that row gets a cell located at target column.
        /// </summary>
        /// <param name="searchColumnName">column name used to search for target row</param>
        /// <param name="searchCellValue">cell value used to search for target row</param>
        /// <param name="targetColumnName">target column to get cell for</param>
        /// <returns><see cref="DataGridCell"/> object as <see cref="IDataGridCell"/></returns>
        /// <exception cref="ControlNotFoundException">if cell was not found</exception>
        public IDataGridCell GetCell(string searchColumnName, string searchCellValue, string targetColumnName) =>
            GetRow(searchColumnName, searchCellValue)
            .GetCell(GetColumnIndex(targetColumnName));

        /// <summary>
        /// Gets table row by its index (zero based)
        /// </summary>
        /// <param name="rowIndex">row index (zero based)</param>
        /// <returns><see cref="DataGridRow"/> object as <see cref="IDataGridRow"/></returns>
        /// <exception cref="ControlNotFoundException">if there is no row at specified index</exception>
        public IDataGridRow GetRow(int rowIndex) =>
            HasRows && (rowIndex < RowsCount) ?
            Rows[rowIndex] :
            throw new ControlNotFoundException($"Row with index '{rowIndex}' does not exist.");

        /// <summary>
        /// Gets row having specified value at specified column.
        /// </summary>
        /// <param name="columnName">column name used to search for target row</param>
        /// <param name="cellValue">cell value used to search for target row</param>
        /// <returns><see cref="DataGridRow"/> object as <see cref="IDataGridRow"/></returns>
        /// <exception cref="ControlNotFoundException">if row was not found</exception>
        public IDataGridRow GetRow(string columnName, string cellValue)
        {
            int columnIndex = GetColumnIndex(columnName);
    
            return GetRowOrDefault(columnIndex, cellValue) ??
                throw new ControlNotFoundException($"Row where '{columnName}' = '{cellValue}' does not exist.");
        }

        /// <summary>
        /// Gets row having specified value at column with specific index (zero based).
        /// </summary>
        /// <param name="columnIndex">column index (zero based)</param>
        /// <param name="cellValue">cell value used to search for target row</param>
        /// <returns><see cref="DataGridRow"/> object as <see cref="IDataGridRow"/></returns>
        /// <exception cref="ControlNotFoundException">if row was not found</exception>
        public IDataGridRow GetRow(int columnIndex, string cellValue) =>
            GetRowOrDefault(columnIndex, cellValue) ?? 
                throw new ControlNotFoundException(
                    $"Row where column with index '{columnIndex}' = '{cellValue}' does not exist.");

        [Obsolete]
        public bool HasCell(string searchColumnName, string searchCellValue, string targetColumnName) =>
            throw new NotImplementedException("Need to remove, it's useless");

        /// <summary>
        /// Checks immediately if column with specified name exists in table.
        /// </summary>
        /// <param name="columnName">column name</param>
        /// <returns>true - if column exists, otherwise not</returns>
        public bool HasColumn(string columnName) =>
            TryGetChild(ByLocator.Xpath(string.Format(ColumnXpathTemplate, columnName)));

        /// <summary>
        /// Checks immediately if row having specified value at specified column exists in table.
        /// </summary>
        /// <param name="columnName">column name used to search for target row</param>
        /// <param name="cellValue">cell value used to search for target row</param>
        /// <returns>true - if row exists, otherwise not</returns>
        public bool HasRow(string columnName, string cellValue)
        {
            int columnIndex = GetColumnIndex(columnName);
            return GetRowOrDefault(columnIndex, cellValue) != null;
        }

        private int GetColumnIndex(string columnName)
        {
            if (columnName == null)
            {
                throw new ArgumentNullException(nameof(columnName), "Column name must not be null");
            }

            IList<WebControl> headers = FindList<WebControl>(ByLocator.Css(ColumnCss));

            for (int i = 0; i < headers.Count; i++)
            {
                if (headers[i].Text.Equals(columnName))
                {
                    return i;
                }
            }

            throw new ControlNotFoundException($"Column '{columnName}' does not exist.");
        }

        private IDataGridRow GetRowOrDefault(int columnIndex, string cellValue)
        {
            if (cellValue == null)
            {
                throw new ArgumentNullException(nameof(cellValue), "Cell value must not be null");
            }

            return HasRows? Rows.FirstOrDefault(r => r.GetCell(columnIndex).Data.Equals(cellValue)) : null;
        }
    }

    public class DataGridRow : WebControl, IDataGridRow
    {
        public IDataGridCell GetCell(int index) => 
            TryGetChild(ByLocator.Css($"td:nth-of-type({index + 1})"), 0, out DataGridCell cell) ?
            cell :
            throw new ControlNotFoundException($"Cell with index '{index}' does not exist.");
    }

    public class DataGridCell : WebControl, IDataGridCell
    {
        public string Data => GetAttribute("innerText").Trim();
    }
}
