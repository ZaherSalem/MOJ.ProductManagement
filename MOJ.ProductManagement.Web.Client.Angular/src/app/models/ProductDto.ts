export interface ProductDto {
    id: number;
    name: string;
    quantityPerUnitId: number;
    reorderLevel: number;
    supplierId: number;
    unitPrice: number;
    unitsInStock: number;
    unitsOnOrder: number;

    supplierName: string;
    quantityPerUnitName: string;
}