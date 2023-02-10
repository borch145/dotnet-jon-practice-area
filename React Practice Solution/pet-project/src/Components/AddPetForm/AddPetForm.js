import * as React from 'react';
import Box from '@mui/material/Box';
import FormControl from '@mui/material/FormControl';
import Input from '@mui/material/Input';
import InputLabel from '@mui/material/InputLabel';
import { Typography } from '@mui/material';
import Button from '@mui/material/Button';
import { PetsContext } from '../../Contexts/PetsContext';
import {useState, useContext} from 'react';
import PetTable from '../PetTable/PetTable';

export default function AddPetForm() {
  const {pets, setPets} = useContext(PetsContext);

  const [name, setPetName] = useState("");
  const [age, setPetAge] = useState("");
  const [species, setPetSpecies] = useState("");

  const handlePetNameChange = (e) => {
      setPetName(e.target.value);
  }
  const handlePetAgeChange = (e) => {
    setPetAge(e.target.value);
  }
  const handlePetSpeciesChange = (e) => {
    setPetSpecies(e.target.value);
  }

  const handleAddPet = () => {
    var petIds = pets.map(pet => pet.id);
    var highestId = Math.max(...petIds);

    var pet = {
      id: highestId +1,
      name: name,
      age: age,
      species: species
    }

    setPets([...pets, pet])
    
  }
 
  return (
    <div>
      <Box
        component="form"
        sx={{
          '& > :not(style)': { m: 1 },
        }}
        noValidate
        autoComplete="off"
      >
        <Typography variant="h6">Enter New Pet Info</Typography>
        <FormControl variant="standard">
          <InputLabel htmlFor="component-simple">Name</InputLabel>
          <Input id="petNameInput" onChange={handlePetNameChange}/>
        </FormControl>
        <FormControl variant="standard">
          <InputLabel htmlFor="component-simple">Age</InputLabel>
          <Input id="petAgeInput" onChange={handlePetAgeChange} />
        </FormControl>
        <FormControl variant="standard">
          <InputLabel htmlFor="component-simple">Species</InputLabel>
          <Input id="petSpeciesInput" onChange={handlePetSpeciesChange} />
        </FormControl>
      </Box>
      <Button color="success" onClick={handleAddPet}>Add Pet</Button>
      <PetTable />
    </div>
  );
}