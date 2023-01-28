import { Box, CircularProgress } from '@mui/material';
import React from 'react'

 const LoadingScreen = () => {
 return (
    <Box sx={{ display: "flex", justifyContent: 'center' }}>
    <CircularProgress size={64}/>
  </Box>
)};

 export default LoadingScreen;