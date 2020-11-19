#encoding : utf-8

from e_base import *

def export_json(xls, fn):
    f = create_file(fn)
    if f != None:
        reader = xls_reader.XLSReader()
        cfgs = reader.GetSheetByIndex(xls, 7, 1)
        if cfgs != None:
            f.write("{\n")
            s = "\t\"GoodsTable\": [\n"
            for c in cfgs:
                ri = RowIndex(len(c))
                ss = "\t\t{\n"
                ss += "\t\t\t\"GoodsId\": \"" + conv_int(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"GoodsName\": \"" + conv_str_bin(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"PurchaseTime\": \"" + conv_flo(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"ShelfLife\": \"" + conv_flo(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"PurchasingPrice\": \"" + conv_flo(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"ImageId\": \"" + conv_int(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"TypeId\": \"" + conv_int(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"SalesLevel\": \"" + conv_int(c[ri.Next()]) + "\"\n"
                ss += "\t\t},\n"
                s += ss
            s = s[:-2]
            s += "\n"
            s += "\t]\n"
            s += "}"
            f.write(s)
        else:
            print('sheed %s get failed.' % 'cfg')
        f.close()
def export_bin(xls, fn):
    pass