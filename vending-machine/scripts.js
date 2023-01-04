let currentMoney = 0.00;
let currentMessage = "Click Money To Buy Stuff!"
displayMessage(currentMessage);

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



function populateItems(){
    //TODO: implement code to populate the page with the items in the inventory
}

function requestVend(id){
    var item = inventory[id];
    
    var message = "";

    if(item === undefined){
        message = `DEV ERROR: Item of id ${id} not found.`
    }
    else if(item.quantity === 0){
        message = `Sorry, ${item.name} is out of stock.`
    }
    else if(currentMoney < item.price){
        message = `${item.price}$ is needed for this item, please enter more money.`;
    }
    else{
        item.quantity--;
        currentMoney -= item.price;

        message = `Vended a ${item.name}, thank you!`;
    }
}
function addDollar(){
    currentMoney += 1.00;
    document.getElementById("currentMoneyDisplay").innerHTML = currentMoney;
    currentMessage = "Dollar Added! Big Spender!";
    displayMessage(currentMessage);
}
function addQuarter(){
    currentMoney += 0.25;
    document.getElementById("currentMoneyDisplay").innerHTML = currentMoney;
    currentMessage = "Quarter Added! That's a gumballs worth!";
    displayMessage(currentMessage);
}
function addDime(){
    currentMoney += 0.10;
    document.getElementById("currentMoneyDisplay").innerHTML = currentMoney;
    currentMessage = "Dime Added! Not even a gumball's worth!";
    displayMessage(currentMessage);
}
function addNickel(){
    currentMoney += 0.05;
    document.getElementById("currentMoneyDisplay").innerHTML = currentMoney;
    currentMessage = "Nickel Added! You've got a long way to go, Kid!";
    displayMessage(currentMessage);
}
function displayMessage(currentMessage){
    document.getElementById("currentMessageDisplay").innerText = `${currentMessage}`
}