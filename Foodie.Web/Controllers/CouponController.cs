using Foodie.Web.Model;
using Foodie.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> coupons = new();
            ResponseDto? response = await _couponService.GetAllCouponAsync();

            if (response != null && response.IsSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
                //TempData["success"] = "Coupons loaded successfully";
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupons);
        }
        public async Task<IActionResult> CreateCoupon()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDto coupon)
        {
            ResponseDto response = await _couponService.CreateCouponAsync(coupon);
            if (response.IsSuccess)
            {
                TempData["success"] = "Coupon created successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupon);
        }

        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            ResponseDto response = await _couponService.GetCouponByIdAsync(couponId);
            if (response.IsSuccess)
            {
                CouponDto coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(coupon);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDto couponDto)
        {
            //ResponseDto response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
            ResponseDto response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
            if (response.Result != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(couponDto);
        }
    }
}
