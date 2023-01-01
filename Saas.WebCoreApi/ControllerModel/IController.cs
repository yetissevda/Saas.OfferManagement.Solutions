using Microsoft.AspNetCore.Mvc;
using Saas.Entities.Generic;

namespace Saas.WebCoreApi.ControllerModel
{
    internal interface IControllerModel

    {

        IActionResult GetList();
        IActionResult GetById(int companyId);
        IActionResult Add(IEntity entity);
        IActionResult Update(IEntity company);
        IActionResult Delete(IEntity company);
        IEntity PrepareForDelete(IEntity entity);

    }
}
