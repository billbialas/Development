import pymssql
conn = pymssql.connect(server='74.86.97.85', user='bialas01', password='Superman01az', database='WebMagic_Prod')
cursor = conn.cursor()
cursor.execute('SELECT [leads_pk],[ld_lname],[ld_fname],[ld_cphone] FROM [WebMagic_Prod].[dbo].[tbl_leads];')
row = cursor.fetchone()
while row:
    print (str(row[0]) + " " + str(row[1]) + " " + str(row[2]))
    row = cursor.fetchone()