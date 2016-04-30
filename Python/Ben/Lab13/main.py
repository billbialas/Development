class Hamburger:

    singlePatty = 5.95
    doublePatty = 7.95
    burgerCost = 0
    extrasCost = 0

    def __init__(self, type):
        self.type= type
        if (type == "single"):
            self.burgerCost = 5.95
        elif (type == "double"):
            self.burgerCost =7.95

    def add_item(self,item):
        if (item=="C"):
            self.extrasCost = self.extrasCost + .75
        elif (item=="B"):
            self.extrasCost = self.extrasCost + 1.00
        elif (item=="O"):
            self.extrasCost = self.extrasCost + .50
        elif (item=="M"):
            self.extrasCost = self.extrasCost + .50


    def get_BurgerCost(self):
        if (self.type == "single"):
            return self.singlePatty
        elif (self.type == "double"):
            return self.doublePatty
        else:
            return 0

    def get_TotalBurgerPrice(self):
        return self.burgerCost + self.extrasCost

    def get_ExtrasCost(self):
        return self.extrasCost

def main():
    quit = False
    while not (quit):
        makeBurger()

        print ("Order another burger?")
        userInput = input ("Yes or No?")
        if (userInput.lower() == "no"):
            quit = True

def makeBurger():
    quit=False

    print ("Please choose the type of burger you would like:\n"
           "\t Single Patty...... $ 5.95\n"
           "\t Double Patty.......$ 7.95\n")
    burgerType = input('single or double?')

    # Instantiate the Hamburger class
    hamburger = Hamburger(burgerType)

    while not (quit):
        print ("Enter your extras:\n"
               "\t C = Cheese\n"
               "\t B = Bacon\n"
               "\t O = Grilled Onions\n"
               "\t M = Grilled Mushrooms\n"
               "\t Q = Quit\n")
        userInput=input("Choice?")
        if (userInput.lower()=="q"):
            quit=True
        else: hamburger.add_item(userInput)

    print (hamburger.get_BurgerCost())
    print (hamburger.get_ExtrasCost())
    print (hamburger.get_TotalBurgerPrice())


# this calls the 'main' function when this script is executed
if __name__ == '__main__': main()