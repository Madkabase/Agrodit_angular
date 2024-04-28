export enum FilterConditionEnum{
    Contains=1,
    NotContains=2,
    Equals=3,
    GreaterThan=4,
    GreaterThanEqual=5,
    LessThan=6,
    LessThanEqual=7,
}
export var filterCondtionArray = ["Equals","Contains","NotContains","GreaterThan","GreaterThanEqual","LessThan","LessThanEqual"];

export function convertStringToFilterCondition (str:string){
    return  FilterConditionEnum[str];
} 
export interface IFilterCondition{
    ColumnName: string;
    ColumnCondition: FilterConditionEnum;
    ColumnValue: string;
}
