import React, { useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import IObject from "../interfaces/IObject";
import { useFilteredArray } from "./FilteredArray";
import classes from './Filter.module.css';

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

  /*
  useEffect(() => {
    onFilter && onFilter(filteredData);
  }, [filterKey, filteredData, data])
*/
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
      <div className={classes.container}>
        <Form.Select className={classes.keyItem} value={filterKey} onChange={handleFilterKeyChange}>
          {keys.map((key) => (
            <option key={key} value={key}>
              {key}
            </option>
          ))}
        </Form.Select>
        <Form.Control
          className={classes.inputItem}
          type="text"
          value={filterString}
          onChange={handleFilterStringChange}
          placeholder="Search for the looking items"
        />
      </div>
    </>
  );
};

export default FilterArrayFunction;
