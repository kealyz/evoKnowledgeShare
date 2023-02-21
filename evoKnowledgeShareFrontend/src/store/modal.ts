import { createSlice } from "@reduxjs/toolkit";

const initModalSlice = {show: false, content: ""};

const modalSlice = createSlice({
    name: "modal",
    initialState: initModalSlice,
    reducers:{
        toggleShow(state){
            state.show = !state.show;
        },
        setContent(state, action){
            state.content = action.payload
        },
        removeModalContent(state){
            state.content = "";
        }
    }
})

export const modalActions = modalSlice.actions;
export default modalSlice.reducer;