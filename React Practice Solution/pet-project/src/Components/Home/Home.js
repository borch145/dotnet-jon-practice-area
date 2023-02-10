import PetTable from "../PetTable/PetTable";
import { Typography } from "@mui/material";

export default function Home(){

    return (
        <>
        <Typography variant="h2">Welcome to Pet-a-dex, the Pet organization solution.</Typography>
        <PetTable />
        </>
    )
}