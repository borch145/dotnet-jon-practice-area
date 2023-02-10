import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import {Link} from 'react-router-dom';

function MyAppBar() {
 
  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Typography variant='h5'>PET-A-DEX</Typography>
            <Button 
                color='inherit' 
                component= {Link} 
                to={'/viewpets'}
                sx={{ml:'50px'}}
                    >View Pets
            </Button>
            <Button 
                color='inherit' 
                component= {Link} 
                to={'/addpet'}
                sx={{ml:'30px'}}
                    >Add Pet
            </Button>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default MyAppBar;