import { PetsContext } from './Contexts/PetsContext';
import {useState} from 'react';
import AddPetForm from './Components/AddPetForm/AddPetForm'
import Layout from "./Components/Layout/Layout.js";
import Home from './Components/Home/Home';
import PetCard from './Components/PetCard/PetCard'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

function App() {

  const startingPets = [
    {
      id: 1,
      name: "rex",
      age: 3,
      species: "labrapoodle",
    },
    {
      id: 2,
      name: "Porkchop",
      age: 12,
      species: "pig",
    }
  ]

  const[pets, setPets] = useState(startingPets);


  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
              {
                  path: "/viewpets",
                  element: <Home/>,
              },
              {
                  path: "/addpet",
                  element: <AddPetForm />
              },
              {
                path: "/viewpet/:id",
                element: <PetCard />
              }
      ]
    }
  ]);
  
  
  return (
    <div className="App">
      {/*Everything within the context provider will be able to pull context value with useContext();*/}
      <PetsContext.Provider value={{pets, setPets}}>
          <RouterProvider router={router} />
      </PetsContext.Provider>
    </div>
  );

}

export default App;
