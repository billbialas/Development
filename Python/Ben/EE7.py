income = float()
taxrate = int()
profit = float()
totaltax = float()

def getIncome():
    income = float(input("How much money do you make on an annual basis?"))
    return income

def getTax(income):
    if income < 20000:
        taxrate = 0
        profit = income
        totaltax = income *0

    elif income > 20000 and income<100000:
        taxrate = '25%'
        profit = income * .75
        totaltax = income * .25

    elif income>100000 and income<250000:
        taxrate = '35%'
        profit = income * .65
        totaltax = income *.35

    elif income>250000:
        taxrate = '45%'
        profit = income *.55
        totaltax = income *.45

    return totaltax

def main():
    income = getIncome()
    print("Your total profit is:",'\n Your total income without tax is:' ,income,'\n Your total tax rate is:','\n Your total deductions:',getTax(income))
if __name__ == '__main__': main()