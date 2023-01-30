import { Add, Remove } from '@mui/icons-material';
import { ButtonGroup, Button, TextField } from '@mui/material';
import React from 'react'
import '../SCSS/Counter.scss'; 

interface ICounter {
    count: number;
    setCount: React.Dispatch<React.SetStateAction<number>>;
} 
 const Counter = ({count, setCount}: ICounter) => {
 return (
    <ButtonGroup>
    <Button
      onClick={() => {
        setCount(Math.max(count - 1, 0));
      }}
      size="small"
    >
      <Remove fontSize="small" />
    </Button>
    <TextField
      value={Number.isNaN(count) ? 0 : count}
      onChange={(e) => {
        setCount(parseInt(e.target.value));
      }}
      className="count"
      type="number"
      size="small"
      inputProps={{ style: { textAlign: "center" } }}
      sx={{
        "& fieldset": { border: "none" },
      }}
    />
    <Button
      variant="contained"
      onClick={() => {
        setCount(count + 1);
      }}
      size="small"
    >
      <Add fontSize="small" />
    </Button>
  </ButtonGroup>
)};

 export default Counter;