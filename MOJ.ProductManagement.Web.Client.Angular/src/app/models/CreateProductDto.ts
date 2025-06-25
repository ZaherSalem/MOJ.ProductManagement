import { eQuantityPerUnit } from "../../Enums/eQuantityPerUnit";

export interface CreateProductDto {
    /**
     * Name of the product (required, max 100 characters)
     */
    name: string;

    /**
     * Unit of measurement for the product
     */
    quantityPerUnitId: number;

    /**
     * Minimum stock level before reordering (must be >= 0)
     */
    reorderLevel: number;

    /**
     * ID of the supplier (required)
     */
    supplierId: number;

    /**
     * Price per unit (must be positive)
     */
    unitPrice: number;

    /**
     * Number of units currently in stock (must be >= 0)
     */
    unitsInStock: number;

    /**
     * Number of units currently on order (must be >= 0)
     */
    unitsOnOrder: number;
}