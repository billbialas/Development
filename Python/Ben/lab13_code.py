type = ""
addonsname = ['Cheese','Bacon','Grilled Onions','Grilled Mushrooms']
addonsprice = [.75, 1, .5, .5]
firstprice = float()
index = 0
totalprice = float()
extras = ""
quantity = float()
quit = ""
endprice = float()

quit = input("Do you want to quit?")
while quit == 'no':

    type = input("single or double patty? Singles are $5.95 and doubles are $7.95")
    if type == "single":
        firstprice = float(5.95)
    elif type == "double":
        firstprice = float(7.95)

    while index < 4:
        extras = input('Do you want '+addonsname[index])
        if extras == "yes":
           totalprice = firstprice + addonsprice[index]
        index +=1
    print("The price of the burger without extras is: ",firstprice,'\n',"The price of the burger with extras is: ",totalprice)

    quantity = int(input("How many burgers do you want?"))
    endprice = quantity * totalprice
    print("The total price of all of your burgers is:",endprice)
    quit = input("Do you want to quit?")
