import React from 'react'
import classes from './Modal.module.css'
import { Fragment } from 'react';
import ReactDOM from 'react-dom';
import IModal from '../interfaces/Modal/IModal';
import IModalOverlay from '../interfaces/Modal/IModalOverlay';
import IBackdrop from '../interfaces/Modal/IBackdrop';

const Backdrop = (props: IBackdrop) => {
    return <div className={classes.backdrop} onClick={props.onClose} />
};

const ModalOverlay = (props: IModalOverlay) => {
    return <div className={classes.modal}>
        <div className={classes.content}>{props.children}</div>
    </div>
};

const portalElement = document.getElementById('overlays')!;

export const Modal = (props: IModal) => {
    return (
            <Fragment>
                {ReactDOM.createPortal(<Backdrop onClose={props.onClose} />, portalElement)}
                {ReactDOM.createPortal(<ModalOverlay>{props.children}</ModalOverlay>, portalElement)}
            </Fragment>
    )
}