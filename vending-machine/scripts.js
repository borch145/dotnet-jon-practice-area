let currentMoney = 0.00;
let currentMessage = ""
let currentSelection = ""
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
        currentMessage = `Please select an Item first! I recommend ${inventory[getRndInteger(1, inventory.length)].name}`
        document.getElementById("foodPlease").play();
    }
    else if(item.quantity === 0){
        currentMessage = `Sorry, ${item.name} is out of stock.`
    }
    else if(currentMoney < item.price){
        currentMessage = `$ ${(item.price).toFixed(2)} is needed for this item, please enter more money.`;
        document.getElementById("goldPlease").play();
    }
    else{
        item.quantity--;
        currentMoney -= item.price;
        document.getElementById("vendSound").play();
        currentMessage = `Vended a ${item.name}, thank you!`;
    }
    displayMessage(currentMessage);
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    renderInventory();
    currentSelection = ""
}
function addDollar(){
    document.getElementById("dollarInsert").play();
    currentMoney += 1.00;
    currentMoney = Math.round(currentMoney*100)/100
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Dollar added! Big spender!";
    displayMessage(currentMessage);
}
function addQuarter(){
    document.getElementById("coinInsert").play();
    currentMoney += 0.25;
    currentMoney = Math.round(currentMoney*100)/100
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Quarter added! That's a gumball's worth!";
    displayMessage(currentMessage);
}
function addDime(){
    document.getElementById("coinInsert").play();
    currentMoney += 0.10;
    currentMoney = Math.round(currentMoney*100)/100
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Dime added! Not even a gumball's worth!";
    displayMessage(currentMessage);
}
function addNickel(){
    document.getElementById("coinInsert").play();
    currentMoney += 0.05;
    currentMoney = Math.round(currentMoney*100)/100
    document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    currentMessage = "Nickel added! You've got a long way to go, kid!";
    displayMessage(currentMessage);
}
function displayMessage(currentMessage){
    document.getElementById("currentMessageDisplay").innerHTML = `${currentMessage}`
}
function renderInventory(){

    document.getElementById("inventoryRenderSpace").innerHTML = ""
    for(i=0; i<inventory.length; i++){

        if(inventory[i].quantity > 0){
            document.getElementById("inventoryRenderSpace").innerHTML += `<div class="col-4"><button type="button" class="btn btn-light" style="margin-top: 10px; width:75%;" onclick="setSelection(${inventory[i].id})">
            <u>${inventory[i].name}</u></br>$ ${inventory[i].price.toFixed(2)}</br></br>Stock: ${inventory[i].quantity}</button></div>`
        }
        else{
            document.getElementById("inventoryRenderSpace").innerHTML += `<div class="col-4"><button type="button" class="btn btn-light" style="margin-top: 10px; width:75%;" disabled>
            <u>${inventory[i].name}</u></br>$ ${inventory[i].price.toFixed(2)}</br></br>Out of Stock</button></div>`
        }
    };
}
function setSelection(id){
    currentSelection = id;
    currentMessage = `${inventory[id].name} Selected!`;
    displayMessage(currentMessage);
}
function getRndInteger(min, max) {
    return Math.floor(Math.random() * (max - min) ) + min;
  }
function returnChange(){
    resetChangeDisplays()
    if(currentMoney>0){
        document.getElementById("coinReturn").play();
        displayChange();
        document.getElementById("currentMoneyDisplay").innerHTML = "$" + (Math.round(currentMoney*100)/100).toFixed(2);
    }
    else{
        document.getElementById("goldPlease").play();
    }
}
function displayChange(){
    
    if(currentMoney>=1){
        var totalDollars = Math.floor((currentMoney/1));
        currentMoney -= totalDollars;
        currentMoney = Math.round(currentMoney*100)/100;
        document.getElementById("dollarChangeDisplay").innerHTML = totalDollars;
    }
    if(currentMoney>=.25){
        var totalQuarters = Math.floor((currentMoney/.25));
        currentMoney -= (totalQuarters*.25).toFixed(2);
        currentMoney = Math.round(currentMoney*100)/100
        document.getElementById("quarterChangeDisplay").innerHTML = totalQuarters;
    }
    if(currentMoney>=.10){
        var totalDimes = Math.floor((currentMoney/.10));
        currentMoney -= (totalDimes*.10).toFixed(2);
        currentMoney = Math.round(currentMoney*100)/100;
        document.getElementById("dimeChangeDisplay").innerHTML = totalDimes;
    }
    if(currentMoney>=.05){
        var totalNickels = Math.floor((currentMoney/.05));
        currentMoney -= (totalNickels*.05).toFixed(2);
        currentMoney = Math.round(currentMoney*100)/100;
        document.getElementById("nickelChangeDisplay").innerHTML = totalNickels;
    }
}
function resetChangeDisplays(){
    document.getElementById("dollarChangeDisplay").innerHTML = 0;
    document.getElementById("quarterChangeDisplay").innerHTML = 0;
    document.getElementById("dimeChangeDisplay").innerHTML = 0;
    document.getElementById("nickelChangeDisplay").innerHTML = 0;
}
