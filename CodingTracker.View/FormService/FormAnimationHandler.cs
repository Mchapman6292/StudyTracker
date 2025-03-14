﻿using Guna.UI2.WinForms;

namespace CodingTracker.View.FormService.FormAnimationHandlers
{
    public class FormAnimationHandler
    {
        private readonly Guna2AnimateWindow _animator;

        public FormAnimationHandler(Form ownerForm)
        {
            _animator = new Guna2AnimateWindow(ownerForm)
            {
            };
        }



        public void AnimateFormTransition(Form currentForm, Form targetForm, Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType animationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_BLEND)
        {
            _animator.TargetForm = targetForm;
            _animator.AnimationType = animationType;
            currentForm.Hide();
            targetForm.Show();
        }
    }
}

