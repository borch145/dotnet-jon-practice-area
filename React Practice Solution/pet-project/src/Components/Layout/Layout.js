import MyAppBar from '../MyAppBar/MyAppBar.js';
import { Outlet } from "react-router-dom";

export default function Layout(){

    return(
        <>
        <MyAppBar />
        <Outlet />
        </>
    );
}