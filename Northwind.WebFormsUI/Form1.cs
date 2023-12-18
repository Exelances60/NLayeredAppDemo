using Northwind.Business.Abstract;
using Northwind.Business.Concreate;
using Northwind.DataAccess.Concreate.EntityFramework;
using Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }
        private void LoadComboBox(ComboBox comboBox, string displayMember, string valueMember)
        {
            comboBox.DataSource = _categoryService.GetAll();
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.SelectedIndex = 0;
        }

        private void clearTextBoxAdd(string method)
        {
            if (method == "add")
            {
                tbxProductName.Clear();
                tbxQuantityPerUnit.Clear();
                tbxStock.Clear();
                tbxUnitPrice.Clear();
                cbxCategoryAdd.SelectedIndex = 0;
            }
            else if (method == "update")
            {
                tbxUpdateName.Clear();
                tbxUpdateQuantityPerUnit.Clear();
                tbxUpdateStock.Clear();
                tbxUpdateUnitPrice.Clear();
                cbxCategoryUpdate.SelectedIndex = 0;
            }

        }

        public Form1()
        {
            InitializeComponent();
            _productService = new ProductManager(new EfProductDal());
            _categoryService = new CategoryManager(new EfCategoryDal());
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();

            LoadComboBox(cbxCategory, "CategoryName", "CategoryId");
            LoadComboBox(cbxCategoryAdd, "CategoryName", "CategoryId");
            LoadComboBox(cbxCategoryUpdate, "CategoryName", "CategoryId");
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCategory.SelectedIndex == 0)
            {
                LoadProducts();
            }
            else
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }

        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbxSearch.Text))
            {
                LoadProducts();
            }
            else
            {
                dgwProduct.DataSource = _productService.GetProductsByName(tbxSearch.Text, Convert.ToInt32(cbxCategory.SelectedValue));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Add(new Product
                {
                    CategoryID = Convert.ToInt32(cbxCategoryAdd.SelectedValue),
                    ProductName = tbxProductName.Text,
                    QuantityPerUnit = tbxQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text),
                });
                MessageBox.Show("Product Added!");
                LoadProducts();
                clearTextBoxAdd("add");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
      
        }
        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgwProduct.Rows[e.RowIndex];
            tbxUpdateName.Text = row.Cells["ProductName"].Value.ToString();
            tbxUpdateQuantityPerUnit.Text = row.Cells["QuantityPerUnit"].Value.ToString();
            tbxUpdateStock.Text = row.Cells["UnitsInStock"].Value.ToString();
            tbxUpdateUnitPrice.Text = row.Cells["UnitPrice"].Value.ToString();
            cbxCategoryUpdate.SelectedValue = row.Cells["CategoryID"].Value;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cbxCategoryUpdate.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a category!");
            }
            else
            {
                _productService.Update(new Product
                {
                    ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells["ProductID"].Value),
                    CategoryID = Convert.ToInt32(cbxCategoryUpdate.SelectedValue),
                    ProductName = tbxUpdateName.Text,
                    QuantityPerUnit = tbxUpdateQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUpdateUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxUpdateStock.Text),
                });
                MessageBox.Show("Product Updated!");
                LoadProducts();
                clearTextBoxAdd("update");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxUpdateName.Text != "" || tbxUpdateQuantityPerUnit.Text != "" || tbxUpdateStock.Text != "" || tbxUpdateUnitPrice.Text != "" || cbxCategoryUpdate.SelectedIndex != 0)
                {
                    _productService.Delete(new Product
                    {
                        ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells["ProductID"].Value)
                    });
                    MessageBox.Show("Product Deleted!");
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Please select a product!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

    }
}
