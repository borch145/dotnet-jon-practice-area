let currentMoney = 0.00;
let currentMessage = ""
let inventory = [
    {
        "id": 0,
        "name": "Hungerbuster",
        "quantity": 9,
        "price" : 1.00
    },
    {
        "id": 1,
        "name": "Bag o' Chips",
        "quantity": 3,
        "price": 1.50
    },
    {
        "id": 2,
        "name": "Dorringos",
        "quantity": 7,
        "price": 0.75
    },
    {
        "id": 3,
        "name": "Salted Pork",
        "quantity": 11,
        "price": 0.60
    },
    {
        "id": 4,
        "name": "Egg",
        "quantity": 0,
        "price": 2.50
    },
    {
        "id": 5,
        "name": "Toaster Pie",
        "quantity": 8,
        "price": 3.25
    },
    {
        "id": 6,
        "name": "Hot Rocket",
        "quantity": 4,
        "price": 1.25
    },
    {
        "id": 7,
        "name": "Yum-bits",
        "quantity": 1,
        "price": 1.35
    },
    {
        "id": 8,
        "name": "Dinner-in-a-can",
        "quantity": 8,
        "price": 5.25
    }
];

renderInventory();

function populateItems(){
    //TODO: implement code to populate the page with the items in the inventory
}

function requestVend(id){
    var item = inventory[id];
    
    if(item === undefined){
        currentMessage = `DEV ERROR: Item of id ${id} not found.`
    }
    else if(item.quantity === 0){
        currentMessage = `Sorry, ${item.name} is out of stock.`
    }
    else if(currentMoney < item.price){
        currentMessage = `${item.price}$ is needed for this item, please enter more money.`;
    }
    else{
        item.quantity--;
        currentMoney -= item.price;

        currentMessage = `Vended a ${item.name}, thank you!`;
    }
    displayMessage(currentMessage);
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
}
function addDollar(){
    currentMoney += 1.00;
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Dollar Added! Big Spender!";
    displayMessage(currentMessage);
}
function addQuarter(){
    currentMoney += 0.25;
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Quarter Added! That's a gumballs worth!";
    displayMessage(currentMessage);
}
function addDime(){
    currentMoney += 0.10;
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Dime Added! Not even a gumball's worth!";
    displayMessage(currentMessage);
}
function addNickel(){
    currentMoney += 0.05;
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Nickel Added! You've got a long way to go, Kid!";
    displayMessage(currentMessage);
}
function displayMessage(currentMessage){
    document.getElementById("currentMessageDisplay").innerText = `${currentMessage}`
}
function renderInventory(){

    for(i=0; i<inventory.length; i++){

        if(inventory[i].quantity > 0){
            document.getElementById("inventoryRenderSpace").innerHTML += `<div class="col-4"><button type="button" class="btn btn-light" style="margin-top: 10px" onclick="requestVend(${inventory[i].id})">
            <u>${inventory[i].name}</u></br>$ ${inventory[i].price.toFixed(2)}</br></br>Stock: ${inventory[i].quantity}</button></div>`
        }
        else{
            document.getElementById("inventoryRenderSpace").innerHTML += `<div class="col-4"><button type="button" class="btn btn-light" style="margin-top: 10px" disabled>
            <u>${inventory[i].name}</u></br>$ ${inventory[i].price.toFixed(2)}</br></br>Out of Stock</button></div>`
        }
    };
}
