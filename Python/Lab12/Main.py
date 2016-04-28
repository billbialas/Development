# Init Arrays
homeOwners=['Carson', 'Smith', 'Jackson', 'Swanson','Perry', 'Beufort','Anderson']
sellingPrice=[125900,115000,105900,85000,150000,155249,97500]

#Functions
def printTitle():
    print("Welcome to the Botany Bay home sales calculator\n"
          "This program will calculate the average selling price of the homes\n"
          "sold in the past year.  It will then determine how many homes sold\n"
          "above the average price and how many homes sold below the average price.\n"
          "============================================================================\n")
def printOwnerSales():
    index=0
    print ("Botany Bay Home Sales\n"
           "*******************************")
    for index in range(len(homeOwners)):
        print ("{0}. {1}\t $ {2:,.2f}".format(index+1,homeOwners[index], sellingPrice[index]))

def avgSellPrice():
    avgSalePrice=0
    for index in range(len(homeOwners)):
        avgSalePrice= avgSalePrice+ sellingPrice[index]
    avgSalePrice = avgSalePrice/len(homeOwners)
    return avgSalePrice

def homesAboveAverage( avgSellPrice):
    homesAboveAvg=0
    for index in range(len(homeOwners)):
        if sellingPrice[index] > avgSellPrice:
            homesAboveAvg +=1
    return homesAboveAvg

def homesBelowAverage( avgSellPrice):
    homesBelowAvg=0
    for index in range(len(homeOwners)):
        if sellingPrice[index] < avgSellPrice:
            homesBelowAvg +=1
    return homesBelowAvg

def highestPricedHome():
    index =0
    higestPriceIndex=0
    higestPrice=sellingPrice[index]
    for index in range(len(homeOwners)):
        if sellingPrice[index] > higestPrice:
            higestPrice = sellingPrice[index]
            higestPriceIndex = index
    return higestPriceIndex


def lowestPricedHome():
    index =0
    lowestPriceIndex=0
    lowestPrice=sellingPrice[index]
    for index in range(len(homeOwners)):
        if sellingPrice[index] < lowestPrice:
            lowestPrice=sellingPrice[index]
            lowestPriceIndex = index
    return lowestPriceIndex

def main():
    highestPrice=highestPricedHome()
    lowestPrice=lowestPricedHome()
    printTitle()
    printOwnerSales()
    print ("\nThe average selling price is: $ {0:,.2f}".format( avgSellPrice()))
    print ("\nThe number of homes selling above average are: ", homesAboveAverage(avgSellPrice()))
    print ("The number of homes selling below average are: ", homesBelowAverage(avgSellPrice()))
    print ("\nThe highest selling house was: $ {0:,.2f}".format(sellingPrice[highestPrice]))
    print ("\t\t Owned by: ",  homeOwners[highestPrice])
    print ("\nThe lowest selling house was: $ {0:,.2f}".format(sellingPrice[lowestPrice]))
    print ("\t\t Owned by: ",  homeOwners[lowestPrice])

# this calls the 'main' function when this script is executed
if __name__ == '__main__': main()