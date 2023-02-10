import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import { PetsContext } from '../../Contexts/PetsContext';
import { useContext } from 'react';
import { Link } from 'react-router-dom';


export default function PetTable() {
  const {pets} = useContext(PetsContext); ///This is how we get the context value   
  
  return (
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} size="small" aria-label="a table of pets">
          <TableHead>
            <TableRow>
              <TableCell>Pet Id</TableCell>
              <TableCell>Pet Name</TableCell>
              <TableCell align="right">Age</TableCell>
              <TableCell align="right">Species</TableCell>
              <TableCell align="left">View Pet</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {pets.map((pets, index) => (
            <TableRow key={index}>
                <TableCell component="th" scope="row">{pets.id}</TableCell>
                <TableCell>{pets.name}</TableCell>
                <TableCell align="right">{pets.age}</TableCell>
                <TableCell align="right">{pets.species}</TableCell>
                <TableCell><Button component={Link} to={'/viewpet/' + pets.id} variant="contained" size="small" align="right">View</Button></TableCell>
            </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    );
  }