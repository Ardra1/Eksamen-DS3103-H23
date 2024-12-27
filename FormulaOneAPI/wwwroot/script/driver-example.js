 import driverModule from "./modules/driverModule.js";

 const driverExample = document.querySelector("#driver-example");

 const imageUrl = "../images/drivers/";

 const driverCard = () => {
    let driverFromModule = driverModule.getAll();
    let driverHtml = "";

    driverFromModule.forEach(driver => {
        driverHtml += `
        <article class="col-12 col-md-6 col-lg-4">
            <div class="card border-0 h-100 ">
                <div class=" bg-light position-absolute p-3 rounded-end-2 rounded-bottom-2 bottom-0 border border-danger border-3 border-start-0 ">
                    <h3>${driver.id} ${driver.name}</h3>
                    <div class=" d-flex justify-content-between">
                        <span class="text-capitalize border-top border-start border-dark p-1 ps-2 rounded">${driver.nationality}</span>
                        <span class="border-bottom border-end border-dark p-1 pe-2 rounded">Age: ${driver.age}</span>
                    </div>
                </div>
                <img class="rounded-2" src="${imageUrl}${driver.image}"/>
            </div>
        </article>`
    });
    driverExample.innerHTML = driverHtml;
}

(() => {
    driverCard()
})();