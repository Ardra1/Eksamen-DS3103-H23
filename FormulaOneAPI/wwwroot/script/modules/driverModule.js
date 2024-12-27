
const driverModule = (() =>{
    const driver = [
        {id: 1, name: "Max Verstappen", age: 26, nationality: "Netherlands", image: "Verstappen.png"}
    ];

    const getAll = () => structuredClone(driver);

    return {getAll};
})();

export default driverModule;