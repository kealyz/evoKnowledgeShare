import { useState, useEffect } from "react";
import IObject from "../interfaces/IObject";

const filterArray = (
  data: IObject[],
  filterKey: string,
  filterString: string
) => {
  if (data && data.length > 0) {
    return data.filter((item) => {
      return item[filterKey]
        ?.toString()
        .toLowerCase()
        .includes(filterString.toLowerCase());
    });
  } else {
    return [] as IObject[];
  }
};

export const useFilteredArray = (
  data: IObject[],
  filterKey: string,
  filterString: string
) => {
  const [filteredData, setFilteredData] = useState(
    filterArray(data, filterKey, filterString)
  );

  useEffect(() => {
    setFilteredData(filterArray(data, filterKey, filterString));
  }, [data, filterKey, filterString]);

  return filteredData;
};
