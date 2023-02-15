import React, { useState } from "react";
import IObject from "../interfaces/IObject";
import { useFilteredArray } from "./FilteredArray";

interface FilterArrayFunctionProps {
  data: IObject[];
  onFilter: (filteredData: IObject[]) => void;
}

const FilterArrayFunction: React.FC<FilterArrayFunctionProps> = ({
  data,
  onFilter,
}) => {
  let keys: string[] = [];
  if (data.length > 0) {
    keys = Object.keys(data[0]);
  }
  const [filterString, setFilterString] = useState("");
  const [filterKey, setFilterKey] = useState<keyof IObject>('id');

  const filteredData = useFilteredArray(data, filterKey.toString(), filterString);
  onFilter(filteredData);

  const handleFilterKeyChange = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    setFilterKey(event.target.value);
  };

  const handleFilterStringChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setFilterString(event.target.value);
  };

  return (
    <>
      <select value={filterKey} onChange={handleFilterKeyChange}>
        {keys.map((key) => (
          <option key={key} value={key}>
            {key}
          </option>
        ))}
      </select>
      <input
        type="text"
        value={filterString}
        onChange={handleFilterStringChange}
      />
    </>
  );
};

export default FilterArrayFunction;
