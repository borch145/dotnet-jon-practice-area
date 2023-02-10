import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import {useParams} from 'react-router-dom';
import {useContext} from 'react';
import { PetsContext } from '../../Contexts/PetsContext';
import { useNavigate } from "react-router-dom";

export default function BasicCard() {
  
    const{pets, setPets} = useContext(PetsContext);
    const{id} = useParams();
    const pet = pets.find(p => p.id == id);
    const navigate = useNavigate();
    const removePet = () => {
        var index = pets.indexOf(pet);
        var tempArr = pets;
        tempArr.splice(index, 1);
        setPets(tempArr);
        navigate("/viewpets")
    }

    return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <Typography>ID: {pet.id}</Typography>
        <Typography>Name: {pet.name}</Typography>
        <Typography>Age: {pet.age}</Typography>
        <Typography>Species: {pet.species}</Typography>
      </CardContent>
      <CardActions>
        <Button color="error" size="small" onClick={removePet}>Remove Pet</Button>
      </CardActions>
    </Card>
  );
}