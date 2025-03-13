using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;
using ProductCatalog.Models.Enums;

namespace ProductCatalog.Services
{
    public class ProductServices : BaseRepository<Product>, IProductServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _context=context;
            _unitOfWork=unitOfWork;
        }

        public async Task<string> CreateNewProduct(CreateProductViewModel model)
        {
            var output = string.Empty;

            var userId = _unitOfWork.UserResolverService.GetUserId();

            if (userId!=null && userId!="")
            {
                var checkRole = await _unitOfWork.UserResolverService.CheckRole(userId, RolesEnum.Admin.ToString());

                if (checkRole)
                {
                    var checkCategory = await _unitOfWork.CategoryServices.GetCategoryById(model.CategoryId);

                    if (checkCategory!=null)
                    {
                        var newProduct = model.Adapt<Product>();
                        newProduct.CreatedBy = userId;
                        newProduct.CreationDate = DateTime.Now;

                        await Add(newProduct);

                        var count = await CommitChanges();

                        if (count==0)
                            output = "Error While Saving";

                    }
                    else
                        output = "Invalid Category";
                }
                else
                    output = "UnAuthorized";
            }
            else
                output = "Please Login Firstly";

            return output;
        }

        public async Task<ReturnListOfProductViewModel> GetAllProducts()
        {
            var output = new ReturnListOfProductViewModel();

            var allProducts = await GetAllWithData();

            var userProducts = allProducts.Where(i => i.StartDate.Date.AddDays(i.Duration)<=DateTime.Now.Date);

            var lstToAddUserData = new List<ProductDataViewModel>();

            var lstToAddAdminData = new List<ProductDataViewModel>();

            if (userProducts.Count()>0)
            {
                foreach (var item in userProducts)
                {
                    var data = new ProductDataViewModel
                    {
                        Name = item.Name,
                        CategoryName = item.Category!=null ? item.Category.Name : "",
                        CategoryId = item.Category!=null ? item.Category.Id : 0,
                        Price =item.Price,
                        CreationDate = item.CreationDate,
                        StartDate = item.StartDate,
                        Duration = item.Duration,
                    };

                    var cratedByName = await _unitOfWork.UserResolverService.GetUserName(item.CreatedBy);

                    data.CreatedBy = cratedByName;

                    lstToAddUserData.Add(data);
                }
            }

            var userId = _unitOfWork.UserResolverService.GetUserId();

            if (userId!=null && userId!="")
            {
                var checkRole = await _unitOfWork.UserResolverService.CheckRole(userId, RolesEnum.Admin.ToString());

                if (checkRole)
                {
                    foreach (var item in allProducts)
                    {
                        var data = new ProductDataViewModel
                        {
                            Name = item.Name,
                            CategoryName = item.Category!=null ? item.Category.Name : "",
                            CategoryId = item.Category!=null ? item.Category.Id : 0,
                            Price =item.Price,
                            CreationDate = item.CreationDate,
                            StartDate = item.StartDate,
                            Duration = item.Duration,
                        };

                        var cratedByName = await _unitOfWork.UserResolverService.GetUserName(item.CreatedBy);

                        data.CreatedBy = cratedByName;

                        lstToAddAdminData.Add(data);
                    }

                    output.ProductData = lstToAddAdminData;

                }
                else
                {
                    output.ProductData = lstToAddUserData;
                }
            }
            else
                output.ProductData = lstToAddUserData;

            return output;
        }

        public async Task<ReturnaProductDataViewModel> GetProductById(int id)
        {
            var output = new ReturnaProductDataViewModel();

            var getProduct = await FindByIdWithData(id);

            if (getProduct!=null)
            {
                var data = new ProductDataViewModel
                {
                    Name = getProduct.Name,
                    CategoryName = getProduct.Category!=null ? getProduct.Category.Name : "",
                    CategoryId = getProduct.Category!=null ? getProduct.Category.Id : 0,
                    Price =getProduct.Price,
                    CreationDate = getProduct.CreationDate,
                    StartDate = getProduct.StartDate,
                    Duration = getProduct.Duration,
                };

                var cratedByName = await _unitOfWork.UserResolverService.GetUserName(getProduct.CreatedBy);

                data.CreatedBy = cratedByName;
            }
            else
                output.Messege = "Invalid Product";

            return output;
        }

        public async Task<string> DeleteProduct(int id)
        {
            var userId = _unitOfWork.UserResolverService.GetUserId();

            if (userId!=null && userId!="")
            {
                var checkRole = await _unitOfWork.UserResolverService.CheckRole(userId, RolesEnum.Admin.ToString());

                if (checkRole)
                {
                    var getProduct = await FindById(id);

                    if (getProduct != null)
                    {
                        var result = await Delete(getProduct);

                        if (result == string.Empty)
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                        return "Invalid Product";
                }
                else
                    return "UnAuthorized";

            }
            else
                return "Please Login Firstly";
        }

        public async Task<ReturnaProductDataViewModel> UpdateProduct(UpdateProductVieModel model)
        {
            var output = new ReturnaProductDataViewModel();
            var userId = _unitOfWork.UserResolverService.GetUserId();

            if (userId!=null && userId!="")
            {
                var checkRole = await _unitOfWork.UserResolverService.CheckRole(userId, RolesEnum.Admin.ToString());

                if (checkRole)
                {
                    var getProduct = await FindById(model.Id);

                    if (getProduct!=null)
                    {
                        var checkCategory = await _unitOfWork.CategoryServices.GetCategoryById(model.CategoryId);

                        if (checkCategory!=null)
                        {
                            try
                            {
                                getProduct.Name = model.Name;
                                getProduct.Price = model.Price;
                                getProduct.Duration = model.Duration;
                                getProduct.CategoryId = model.CategoryId;
                                getProduct.StartDate = getProduct.StartDate;

                                await Update(getProduct);

                                var count = await CommitChanges();

                                if (count==0)
                                    output.Messege = "Error While Saving";
                                else
                                {
                                    var data = new ProductDataViewModel
                                    {
                                        Name = model.Name,
                                        Price = model.Price,
                                        Duration = model.Duration,
                                        CategoryId = model.CategoryId,
                                        CategoryName = checkCategory.Name,
                                        StartDate = getProduct.StartDate,
                                        CreationDate = getProduct.CreationDate,
                                    };

                                    var cratedByName = await _unitOfWork.UserResolverService.GetUserName(getProduct.CreatedBy);

                                    data.CreatedBy = cratedByName;

                                    output.Product = data;
                                }
                            }
                            catch(Exception ex)
                            {
                                output.Messege = ex.Message.ToString();
                            }
                        }
                        else
                            output.Messege = "Invalid Category";
                    }
                    else
                        output.Messege = "Invalid Product";
                }
                else
                    output.Messege = "UnAuthorized";

            }
            else
                output.Messege = "Please Login Firstly";

            return output;
        }
    }
}
