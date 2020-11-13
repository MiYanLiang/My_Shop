#encoding : utf-8

from e_base import *

def export_json(xls, fn):
    f = create_file(fn)
    if f != None:
        reader = xls_reader.XLSReader()
        cfgs = reader.GetSheetByIndex(xls, 5, 1)
        if cfgs != None:
            f.write("{\n")
            s = "\t\"SogoTable\": [\n"
            for c in cfgs:
                ri = RowIndex(len(c))
                ss = "\t\t{\n"
                ss += "\t\t\t\"SogoId\": \"" + conv_int(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"SogoName\": \"" + conv_str_bin(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"Date_in_produced\": \"" + conv_str_bin(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"Shelf_life\": \"" + conv_int(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"Purchasing_price\": \"" + conv_flo(c[ri.Next()]) + "\",\n"
                ss += "\t\t\t\"Image_id\": \"" + conv_int(c[ri.Next()]) + "\"\n"
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